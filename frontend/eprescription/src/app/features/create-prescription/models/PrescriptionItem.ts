export interface PrescriptionItem {
    id: number;
    Dose: string;// e.g. "500 mg"
    prescriptionId: number;
    Form: string; // e.g. "Tablet"
    drugId: number;
    drugName:string;
    frequency: string;    // e.g. "BID"  
    duration: string;// e.g. "7 days"
    instructions: string; //e.g. after meal
}