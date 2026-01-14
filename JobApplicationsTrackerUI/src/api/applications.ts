import { apiFetch } from "./http";
import { API_ENDPOINT_JOB_APPLICATIONS } from "./const";

export function getApplications() {
  return apiFetch(API_ENDPOINT_JOB_APPLICATIONS);
}