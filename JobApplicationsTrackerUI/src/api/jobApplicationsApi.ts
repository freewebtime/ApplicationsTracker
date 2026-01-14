import type { JobApplication } from "../models/JobApplication";
import { apiFetch } from "./http";

export async function getJobApplications(): Promise<JobApplication[]> {
  return apiFetch("/api/jobapplications");
}

export async function createJobApplication(
  app: Omit<JobApplication, "id">
): Promise<JobApplication> {
  return apiFetch("/api/jobapplications", {
    method: "POST",
    body: JSON.stringify(app),
  });
}

export async function updateJobApplication(
  id: number,
  app: Partial<JobApplication>
): Promise<void> {
  return apiFetch(`/api/jobapplications/${id}`, {
    method: "PUT",
    body: JSON.stringify(app),
  });
}

export async function deleteJobApplication(id: number): Promise<void> {
  return apiFetch(`/api/jobapplications/${id}`, {
    method: "DELETE",
  });
}
