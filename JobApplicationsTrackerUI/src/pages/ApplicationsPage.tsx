import { useEffect, useState } from "react";
import { getApplications } from "../api/applications";

export default function ApplicationsPage() {
  const [apps, setApps] = useState<any[]>([]);

  useEffect(() => {
    getApplications().then(setApps);
  }, []);

  return (
    <div>
      <h1>Applications</h1>
      <pre>{JSON.stringify(apps, null, 2)}</pre>
    </div>
  );
}
