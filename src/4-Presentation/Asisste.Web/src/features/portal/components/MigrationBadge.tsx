import clsx from "clsx";
import type { PortalPhase } from "../types";

interface MigrationBadgeProps {
  phase: PortalPhase;
  className?: string;
}

const badgeLabel: Record<PortalPhase, string> = {
  connected: "Operativo",
  portal: "Portal listo"
};

const badgeTone: Record<PortalPhase, string> = {
  connected: "border border-emerald-200 bg-emerald-50 text-emerald-700",
  portal: "border border-amber-200 bg-amber-50 text-amber-800"
};

export function MigrationBadge({ phase, className }: MigrationBadgeProps) {
  return (
    <span
      className={clsx(
        "inline-flex items-center rounded-full px-3 py-1 text-[0.68rem] font-bold uppercase tracking-[0.26em]",
        badgeTone[phase],
        className
      )}
    >
      {badgeLabel[phase]}
    </span>
  );
}