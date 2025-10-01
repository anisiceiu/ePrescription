import { PrescriptionItem } from "./PrescriptionItem";

export interface PrescriptionModel {
  chiefComplaint:string[],
  otherPastHistory:string[],
  onExamination:string[],
  investigation:string[],
  advice:string[],
  rx:PrescriptionItem[]
}