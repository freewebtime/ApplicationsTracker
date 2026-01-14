export interface JobApplication {
  id: string;
  companyName: string;
  position: string;
  status: string;
  appliedAt: string; // ISO string
  notes?: string;
}