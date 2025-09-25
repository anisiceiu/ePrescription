export interface Prescription {
  id: number;
  patientId: number;
  date: Date;
  notes?: string;
  status?: string;
}