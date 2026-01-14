import { useState } from "react";
import type { JobApplication } from "../models/JobApplication";

type Props = {
  initial?: Partial<JobApplication>;
  onSubmit: (data: Omit<JobApplication, "id">) => void;
};

export function JobApplicationForm({ initial = {}, onSubmit }: Props) {
  const [companyName, setCompanyName] = useState(initial.companyName ?? "");
  const [position, setPosition] = useState(initial.position ?? "");
  const [status, setStatus] = useState(initial.status ?? "");

  function submit(e: React.FormEvent) {
    e.preventDefault();
    onSubmit({
      companyName,
      position,
      status,
      appliedAt: new Date().toISOString(),
    });
  }

  return (
    <form onSubmit={submit}>
      <input value={companyName} onChange={e => setCompanyName(e.target.value)} />
      <input value={position} onChange={e => setPosition(e.target.value)} />
      <input value={status} onChange={e => setStatus(e.target.value)} />
      <button type="submit">Save</button>
    </form>
  );
}
