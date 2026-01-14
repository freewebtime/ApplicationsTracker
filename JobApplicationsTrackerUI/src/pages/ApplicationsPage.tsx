import { useEffect, useState } from "react";
import type { JobApplication } from "../models/JobApplication";
import { getJobApplications, deleteJobApplication } from "../api/applications";

export default function ApplicationsPage() {
  const [applications, setApplications] = useState<JobApplication[]>([]);
  const [loading, setLoading] = useState(true);

  async function handleDelete(id: string) {
    if (!confirm("Delete this application?")) return;

    setLoading(true);
    try {
      await deleteJobApplication(id);
      const updatedApps = await getJobApplications();
      setApplications(updatedApps);
    } catch (err) {
      console.error("Failed to delete:", err);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    getJobApplications()
      .then(setApplications)
      .finally(() => setLoading(false));
  }, []);

  if (loading)
    return (<div>loading...</div>);

  return (
    <div>
      <h1>Applications</h1>
      <table>
        <thead>
          <tr>
            <th>Company</th>
            <th>Position</th>
            <th>Status</th>
            <th>Applied</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {applications.map(app => (
            <tr key={app.id}>
              <td>{app.companyName}</td>
              <td>{app.position}</td>
              <td>{app.status}</td>
              <td>{new Date(app.appliedAt).toLocaleDateString()}</td>
              <td>
                <button>Edit</button>
                <button onClick={() => handleDelete(app.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
