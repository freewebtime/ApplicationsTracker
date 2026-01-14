import { apiFetch, apiRequest } from "./http";
import { API_ENDPOINT_JOB_APPLICATIONS } from "./const";

export function getJobApplications() {
  return apiFetch(API_ENDPOINT_JOB_APPLICATIONS);
}

export function deleteJobApplication(id: string) {
  return apiRequest(`${API_ENDPOINT_JOB_APPLICATIONS}/${id}`, {method: "DELETE"});
}