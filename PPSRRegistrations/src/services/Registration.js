import apiClient from './ApiClient';

export const uploadCsvFile = async (file) => {
  const formData = new FormData();
  formData.append("file", file);
  const response = await apiClient.post(`/api/registration/csv`, formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
}