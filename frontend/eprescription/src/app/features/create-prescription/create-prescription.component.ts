import { Component } from '@angular/core';
import { MaterialModule } from '../../shared/material.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';

@Component({
  selector: 'app-create-prescription',
  imports: [MaterialModule, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './create-prescription.component.html',
  styleUrl: './create-prescription.component.css'
})
export class CreatePrescriptionComponent {
  patientForm: FormGroup;
  data = {
    chiefComplaints: [
      "Fever for 3 days",
      "Headache with nausea",
      "Chest pain on exertion",
      "Shortness of breath",
      "Abdominal pain after meals",
      "Cough with sputum",
      "Dizziness and weakness",
      "Swelling of legs",
      "Loss of appetite",
      "Joint pain and stiffness"
    ],
    otherHistory: [
      "Hypertension for 5 years",
      "Diabetes Mellitus for 8 years",
      "History of tuberculosis treatment",
      "Previous myocardial infarction",
      "Asthma since childhood",
      "History of stroke 2 years ago",
      "Chronic kidney disease on dialysis",
      "Thyroid disorder on medication",
      "History of peptic ulcer disease",
      "Surgical history: Appendectomy"
    ],
    onExamination: [
      "Blood pressure: 140/90 mmHg",
      "Pulse: 88 bpm, regular",
      "Respiratory rate: 20/min",
      "Temperature: 101°F",
      "Pallor present",
      "Cyanosis absent",
      "Edema: mild bilateral leg edema",
      "Heart sounds: S1 S2 normal, no murmur",
      "Lung auscultation: vesicular breath sounds",
      "Abdomen: soft, mild tenderness in epigastrium"
    ],
    investigations: [
      "Complete Blood Count (CBC)",
      "Urine Routine Examination",
      "Liver Function Test (LFT)",
      "Renal Function Test (RFT)",
      "Blood Sugar: Fasting and Postprandial",
      "Lipid Profile",
      "Chest X-Ray",
      "ECG",
      "Ultrasound Abdomen",
      "Thyroid Function Test (TFT)"
    ],
    advice: [
      "Drink plenty of water",
      "Take adequate rest",
      "Follow a balanced diet",
      "Avoid smoking and alcohol",
      "Exercise regularly",
      "Monitor blood pressure daily",
      "Take medicines on time",
      "Maintain personal hygiene",
      "Avoid stress and anxiety",
      "Return for follow-up in 1 week"
    ]
  };

  ccControl = new FormControl('');
  ccOptions: string[] = this.data.chiefComplaints;
  filteredCC: Observable<string[]> = null!;
  ccList: string[] = [];

  ohControl = new FormControl('');
  ohOptions: string[] = this.data.otherHistory;
  filteredOH: Observable<string[]> = null!;
  ohList: string[] = [];

  oeControl = new FormControl('');
  oeOptions: string[] = this.data.onExamination;
  invControl = new FormControl('');
  invOptions: string[] = this.data.investigations;
  advControl = new FormControl('');
  advOptions: string[] = this.data.advice;

  constructor(private fb: FormBuilder) {
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(0)]],
      sex: ['', Validators.required],
      date: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.filteredCC = this.ccControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterCC(value || '')),
    );

    this.filteredOH = this.ohControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterOH(value || '')),
    );
  }

  private _filterCC(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.ccOptions.filter(cc => this._normalizeValue(cc).includes(filterValue));
  }

  private _filterOH(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.ohOptions.filter(cc => this._normalizeValue(cc).includes(filterValue));
  }

  private _normalizeValue(value: string): string {
    return value.toLowerCase().replace(/\s/g, '');
  }

  onOptionSelected(event: any, type: string) {

    if (type == "CC") {
      if (!this.ccList.includes(event.option.value)) {
        this.ccList.push(event.option.value);
      }
      this.ccControl.setValue(null);
    }
    else if (type == "OH")
    {
      if (!this.ohList.includes(event.option.value)) {
        this.ohList.push(event.option.value);
      }
      this.ohControl.setValue(null);
    }

  }

}
