import { useEffect, useState } from "react";
import { profileApi } from "../api/profileApi";
import type { Profile, ProfileDraft, ProfileEditorMode } from "../types";

const pageSize = 10;

export function useProfiles() {
  const [profiles, setProfiles] = useState<Profile[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [editorMode, setEditorMode] = useState<ProfileEditorMode>("create");
  const [editingProfileId, setEditingProfileId] = useState<number | null>(null);
  const [draft, setDraft] = useState<ProfileDraft>({ description: "" });

  useEffect(() => {
    void loadProfiles();
  }, []);

  const totalPages = Math.max(1, Math.ceil(profiles.length / pageSize));
  const firstItemIndex = (currentPage - 1) * pageSize;
  const currentPageItems = profiles.slice(firstItemIndex, firstItemIndex + pageSize);

  useEffect(() => {
    if (currentPage > totalPages) {
      setCurrentPage(totalPages);
    }
  }, [currentPage, totalPages]);

  async function loadProfiles() {
    setIsLoading(true);
    setErrorMessage(null);

    try {
      const data = await profileApi.list();
      setProfiles(data);
      setCurrentPage(1);
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    } finally {
      setIsLoading(false);
    }
  }

  function openCreateEditor() {
    setEditorMode("create");
    setEditingProfileId(null);
    setDraft({ description: "" });
    setErrorMessage(null);
  }

  function openEditEditor(profile: Profile) {
    setEditorMode("edit");
    setEditingProfileId(profile.id);
    setDraft({ description: profile.description });
    setErrorMessage(null);
  }

  function cancelEditor() {
    openCreateEditor();
  }

  function updateDraftDescription(description: string) {
    setDraft({ description });
  }

  async function submitDraft() {
    setIsSubmitting(true);
    setErrorMessage(null);

    try {
      if (editorMode === "create") {
        const createdProfile = await profileApi.create(draft);
        const nextProfiles = [...profiles, createdProfile].sort(sortProfiles);
        setProfiles(nextProfiles);
        setCurrentPage(Math.max(1, Math.ceil(nextProfiles.length / pageSize)));
      } else if (editingProfileId !== null) {
        const updatedProfile = await profileApi.update(editingProfileId, draft);
        setProfiles((currentProfiles) =>
          currentProfiles
            .map((profile) => (profile.id === editingProfileId ? updatedProfile : profile))
            .sort(sortProfiles)
        );
      }

      openCreateEditor();
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    } finally {
      setIsSubmitting(false);
    }
  }

  async function deleteProfile(profile: Profile) {
    const confirmed = window.confirm(`¿Deseas eliminar el perfil ${profile.description}?`);
    if (!confirmed) {
      return;
    }

    setIsSubmitting(true);
    setErrorMessage(null);

    try {
      await profileApi.remove(profile.id);
      setProfiles((currentProfiles) => currentProfiles.filter((item) => item.id !== profile.id));
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    } finally {
      setIsSubmitting(false);
    }
  }

  function goToPreviousPage() {
    setCurrentPage((page) => Math.max(1, page - 1));
  }

  function goToNextPage() {
    setCurrentPage((page) => Math.min(totalPages, page + 1));
  }

  return {
    profiles,
    currentPageItems,
    currentPage,
    totalPages,
    isLoading,
    isSubmitting,
    errorMessage,
    editorMode,
    editingProfileId,
    draft,
    canGoToPreviousPage: currentPage > 1,
    canGoToNextPage: currentPage < totalPages,
    loadProfiles,
    openCreateEditor,
    openEditEditor,
    cancelEditor,
    updateDraftDescription,
    submitDraft,
    deleteProfile,
    goToPreviousPage,
    goToNextPage
  };
}

function sortProfiles(left: Profile, right: Profile) {
  return left.description.localeCompare(right.description, "es", { sensitivity: "base" });
}

function getErrorMessage(error: unknown) {
  return error instanceof Error ? error.message : "Ocurrió un error inesperado.";
}