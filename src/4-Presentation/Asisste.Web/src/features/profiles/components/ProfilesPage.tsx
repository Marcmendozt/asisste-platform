import type { AuthenticatedSession } from "../../auth/types";
import { MigrationBadge } from "../../portal/components/MigrationBadge";
import { useProfiles } from "../hooks/useProfiles";
import type { Profile } from "../types";

interface ProfilesPageProps {
  session: AuthenticatedSession;
}

export function ProfilesPage({ session }: ProfilesPageProps) {
  const {
    currentPageItems,
    currentPage,
    totalPages,
    isLoading,
    isSubmitting,
    errorMessage,
    editorMode,
    draft,
    canGoToPreviousPage,
    canGoToNextPage,
    openCreateEditor,
    openEditEditor,
    cancelEditor,
    updateDraftDescription,
    submitDraft,
    deleteProfile,
    goToPreviousPage,
    goToNextPage
  } = useProfiles();

  const isEditing = editorMode === "edit";

  return (
    <div className="space-y-6">
      <section className="glass-panel p-6 sm:p-8">
        <div className="flex flex-col gap-6 xl:flex-row xl:items-start xl:justify-between">
          <div className="max-w-3xl">
            <div className="flex flex-wrap items-center gap-3">
              <span className="pill-note">Maestros/Perfil</span>
              <MigrationBadge phase="connected" />
            </div>

            <h2 className="mt-4 font-display text-3xl text-slate-950 sm:text-4xl">Perfiles</h2>
            <p className="mt-4 text-base leading-7 text-slate-600">
              Este slice ya reemplaza la vista legacy con React + TypeScript y consume el nuevo endpoint
              operativo del API moderno. El CRUD sigue conectado a SQL Server mediante la capa de
              infraestructura migrada.
            </p>
          </div>

          <div className="grid gap-3 sm:grid-cols-2 xl:w-[360px] xl:grid-cols-1">
            <div className="soft-panel p-4">
              <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Sesión actual</p>
              <p className="mt-3 text-lg font-semibold text-slate-950">{session.fullName}</p>
              <p className="mt-1 text-sm text-slate-500">{session.username}</p>
            </div>

            <div className="soft-panel p-4">
              <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Contexto operativo</p>
              <dl className="mt-3 space-y-2 text-sm text-slate-600">
                <div className="flex items-center justify-between gap-4">
                  <dt>Entrada</dt>
                  <dd className="font-semibold text-slate-900">{formatEntryTime(session.entryTime)}</dd>
                </div>
                <div className="flex items-center justify-between gap-4">
                  <dt>Tolerancia</dt>
                  <dd className="font-semibold text-slate-900">{session.toleranceMinutes} min</dd>
                </div>
                <div className="flex items-center justify-between gap-4">
                  <dt>Móvil</dt>
                  <dd className="font-semibold text-slate-900">
                    {session.hasMobileSession ? "Sesión activa" : "Sin sesión"}
                  </dd>
                </div>
              </dl>
            </div>
          </div>
        </div>
      </section>

      <section className="grid gap-6 xl:grid-cols-[1.45fr_0.85fr]">
        <article className="soft-panel p-6 sm:p-7">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-start sm:justify-between">
            <div>
              <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Catálogo conectado</p>
              <h3 className="mt-3 font-display text-2xl text-slate-950">Listado activo</h3>
              <p className="mt-2 max-w-2xl text-sm leading-6 text-slate-600">
                Gestión paginada sobre perfiles reales con altas, edición y bajas respaldadas por el API.
              </p>
            </div>

            <button className="btn-primary" type="button" onClick={openCreateEditor}>
              Nuevo perfil
            </button>
          </div>

          {errorMessage ? (
            <div className="mt-5 rounded-[24px] border border-rose-200 bg-rose-50 px-5 py-4 text-sm font-semibold text-rose-700">
              {errorMessage}
            </div>
          ) : null}

          {isLoading ? (
            <div className="mt-6 rounded-[28px] border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center text-sm font-semibold text-slate-500">
              Cargando perfiles...
            </div>
          ) : currentPageItems.length === 0 ? (
            <div className="mt-6 rounded-[28px] border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center text-sm font-semibold text-slate-500">
              No hay perfiles disponibles todavía.
            </div>
          ) : (
            <>
              <div className="mt-6 overflow-x-auto rounded-[28px] border border-slate-200">
                <table className="min-w-full border-collapse">
                  <thead className="bg-slate-50 text-left text-[0.72rem] font-bold uppercase tracking-[0.28em] text-slate-500">
                    <tr>
                      <th className="px-5 py-4">#</th>
                      <th className="px-5 py-4">Descripción</th>
                      <th className="px-5 py-4">Estado</th>
                      <th className="px-5 py-4">Acciones</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-200 bg-white">
                    {currentPageItems.map((profile, index) => (
                      <ProfileRow
                        key={profile.id}
                        profile={profile}
                        rowNumber={(currentPage - 1) * 10 + index + 1}
                        onEdit={openEditEditor}
                        onDelete={deleteProfile}
                      />
                    ))}
                  </tbody>
                </table>
              </div>

              <div className="mt-5 flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
                <p className="text-sm font-semibold text-slate-500">
                  Página {currentPage} de {totalPages}
                </p>

                <div className="flex flex-wrap gap-3">
                  <button className="btn-secondary" type="button" onClick={goToPreviousPage} disabled={!canGoToPreviousPage}>
                    Anterior
                  </button>
                  <button className="btn-secondary" type="button" onClick={goToNextPage} disabled={!canGoToNextPage}>
                    Siguiente
                  </button>
                </div>
              </div>
            </>
          )}
        </article>

        <aside className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Edición segura</p>
          <h3 className="mt-3 font-display text-2xl text-slate-950">{isEditing ? "Editar perfil" : "Crear perfil"}</h3>
          <p className="mt-2 text-sm leading-6 text-slate-600">
            La validación pasó del controlador legacy y del JavaScript ad hoc a la capa de aplicación del
            nuevo backend.
          </p>

          <form
            className="mt-6 space-y-5"
            onSubmit={(event) => {
              event.preventDefault();
              void submitDraft();
            }}
          >
            <label className="field-shell" htmlFor="profile-description">
              Descripción
              <input
                id="profile-description"
                className="field-input"
                value={draft.description}
                onChange={(event) => updateDraftDescription(event.target.value)}
                placeholder="Ingresar descripción"
                autoComplete="off"
              />
            </label>

            <div className="rounded-[24px] bg-slate-50 px-4 py-4 text-sm leading-6 text-slate-600">
              Mínimo 4 caracteres. Los nombres duplicados siguen siendo rechazados por el servicio.
            </div>

            <div className="flex flex-wrap gap-3">
              <button className="btn-primary" type="submit" disabled={isSubmitting}>
                {isSubmitting ? "Guardando..." : isEditing ? "Actualizar" : "Guardar"}
              </button>
              <button className="btn-secondary" type="button" onClick={cancelEditor} disabled={isSubmitting}>
                Cancelar
              </button>
            </div>
          </form>
        </aside>
      </section>
    </div>
  );
}

function formatEntryTime(entryTime: string) {
  const [time] = entryTime.split(".");
  return time.length >= 5 ? time.slice(0, 5) : entryTime;
}

interface ProfileRowProps {
  profile: Profile;
  rowNumber: number;
  onEdit: (profile: Profile) => void;
  onDelete: (profile: Profile) => void | Promise<void>;
}

function ProfileRow({ profile, rowNumber, onEdit, onDelete }: ProfileRowProps) {
  return (
    <tr>
      <td className="px-5 py-4 text-sm font-semibold text-slate-500">{rowNumber}</td>
      <td className="px-5 py-4 text-sm font-semibold text-slate-900">{profile.description}</td>
      <td className="px-5 py-4">
        <span
          className={
            profile.isActive
              ? "inline-flex rounded-full bg-emerald-100 px-3 py-1 text-xs font-bold uppercase tracking-[0.24em] text-emerald-700"
              : "inline-flex rounded-full bg-slate-100 px-3 py-1 text-xs font-bold uppercase tracking-[0.24em] text-slate-600"
          }
        >
          {profile.isActive ? "Activo" : "Inactivo"}
        </span>
      </td>
      <td className="px-5 py-4">
        <div className="flex flex-wrap gap-3">
          <button
            className="text-sm font-semibold text-asisste-700 transition hover:text-asisste-900"
            type="button"
            onClick={() => onEdit(profile)}
          >
            Editar
          </button>
          <button
            className="text-sm font-semibold text-rose-700 transition hover:text-rose-800"
            type="button"
            onClick={() => void onDelete(profile)}
          >
            Eliminar
          </button>
        </div>
      </td>
    </tr>
  );
}