import { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { uploadCsvFile } from "./services/Registration";

export default function App() {
  const [summary, setSummary] = useState(null);
  const [error, setError] = useState(null);

  const handleFileChange = async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    try {
      const data = await uploadCsvFile(file);
      setSummary(data);
      setError(null);
    } catch (err) {
      if (err.response && err.response.status !== 200 && (err.response.data.message !== undefined && err.response.data.message !== "")) {
        setError(err.response.data.message);
        setSummary(null);
      } else {
        setError("An error occurred while uploading the file.");
        setSummary(null);
      }
    }

    e.target.value = null;
  };

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Upload CSV File</h1>
      <label className="btn btn-primary">
        Upload a CSV file to the server
        <input type="file" accept=".csv" hidden onChange={handleFileChange} />
      </label>
      {summary && (
        <div className="mt-4">
          <h4>Operation summary</h4>
          <ul>
            <li>Number of submitted records: {summary.submitted ?? 0}</li>
            <li>Number of invalid records: {summary.invalid ?? 0}</li>
            <li>Number of processed records: {summary.processed ?? 0}</li>
            <li>Number of updated records: {summary.updated ?? 0}</li>
            <li>Number of added records: {summary.added ?? 0}</li>
          </ul>
        </div>
      )}
      {error && (
        <div className="mt-4 alert alert-danger" role="alert">
          {error}
        </div>
      )}
    </div>
  );
}