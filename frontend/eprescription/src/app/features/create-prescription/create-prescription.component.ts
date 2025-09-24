import { Component } from '@angular/core';
import { MaterialModule } from '../../shared/material.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { map, Observable, of, startWith } from 'rxjs';
import { HttpService } from '../../core/services/http.service';

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

  frequencies: string[] = [
    '1+0+0', '0+1+0', '0+0+1',
    '1+1+0', '1+0+1', '0+1+1',
    '1+1+1', '½+½+0', '½+0+½',
    '0+½+½', '½+½+½', '2+0+0',
    '0+2+0', '0+0+2', '2+1+0',
    '1+2+1', 'SOS', 'HS', 'OD',
    'BD', 'TDS', 'QID'
  ];

  instructions: string[] = [
    'After meal',
    'Before meal',
    'With meal',
    'Empty stomach',
    'At bedtime',
    'Every 6 hours',
    'Every 8 hours',
    'Every 12 hours',
    'Once daily in the morning',
    'Once daily at night',
    'As needed (SOS)',
    'Immediately after food',
    'Before sleep',
    'With a full glass of water',
    'Do not take with milk',
    'Avoid alcohol',
    'Take with food to avoid gastric upset',
    'Take on an empty stomach for better absorption',
    'Take at the same time every day',
    'Do not stop abruptly without doctor’s advice'
  ];

  durations: string[]=[
    "3 days",
    "5 days",
    "7 days",
    "10 days",
    "14 days (2 weeks)",
    "21 days (3 weeks)",
    "1 month",
    "2 months",
    "3 months",
    "6 months",
    "As directed by physician",
    "Until symptoms resolve",
    "Long term (chronic use)"
  ];

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
  filteredOE: Observable<string[]> = null!;
  oeList: string[] = [];

  invControl = new FormControl('');
  invOptions: string[] = this.data.investigations;
  filteredInv: Observable<string[]> = null!;
  invList: string[] = [];

  advControl = new FormControl('');
  advOptions: string[] = this.data.advice;
  filteredAdv: Observable<string[]> = null!;
  advList: string[] = [];

  constructor(private fb: FormBuilder, private http: HttpService) {
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(0)]],
      sex: ['', Validators.required],
      date: ['', Validators.required]
    });

    http.get<any[]>('DosageForm').subscribe(data => {
      this.dosageForms = data.map(d => d.name);
    });

    http.get<any[]>('Drugs').subscribe(data => {
      this.drugs = data;
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

    this.filteredOE = this.oeControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterOE(value || '')),
    );

    this.filteredInv = this.invControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterInv(value || '')),
    );

    this.filteredAdv = this.advControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterAdv(value || '')),
    );

    this.filteredDosage = this.control.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );


    this.filteredDrug = this.controlDrug.valueChanges.pipe(
      startWith<any | string | null>(''),   // start with empty string instead of null
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filterDrug(name) : this.drugs.slice();
      })
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

  private _filterOE(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.oeOptions.filter(cc => this._normalizeValue(cc).includes(filterValue));
  }

  private _filterInv(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.invOptions.filter(cc => this._normalizeValue(cc).includes(filterValue));
  }

  private _filterAdv(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.advOptions.filter(adv => this._normalizeValue(adv).includes(filterValue));
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
    else if (type == "OH") {
      if (!this.ohList.includes(event.option.value)) {
        this.ohList.push(event.option.value);
      }
      this.ohControl.setValue(null);
    }

    else if (type == "OE") {
      if (!this.oeList.includes(event.option.value)) {
        this.oeList.push(event.option.value);
      }
      this.oeControl.setValue(null);
    }

    else if (type == "INV") {
      if (!this.invList.includes(event.option.value)) {
        this.invList.push(event.option.value);
      }
      this.invControl.setValue(null);
    }
    else if (type == "ADV") {
      if (!this.advList.includes(event.option.value)) {
        this.advList.push(event.option.value);
      }
      this.advControl.setValue(null);
    }
  }


  //Test UI
  control = new FormControl('');
  dosageForms: string[] = [];
  filteredDosage: Observable<string[]> = null!;
  private _filter(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.dosageForms.filter(dosage => this._normalizeValue(dosage).includes(filterValue));
  }

  controlDrug = new FormControl<any | string>('');
  drugs: any[] = [];
  filteredDrug: Observable<any[]> = null!;
  private _filterDrug(name: string): any[] {
    const filterValue = this._normalizeValue(name);
    return this.drugs.filter(drug => this._normalizeValue(drug.name).includes(filterValue));
  }

  displayFn(drug: any): string {
    return drug && drug.name ? drug.name + " " + drug.strength + " " + drug.unit : '';
  }

  frequencyCtrl = new FormControl('');
  instructionCtrl = new FormControl('');
  durationCtrl = new FormControl('');

  onAddClick()
  {
    let dosageForm=this.control.value;
    let drug = this.controlDrug.value;
    let frequency = this.frequencyCtrl.value;
    let duration = this.durationCtrl.value;
    let instruction = this.instructionCtrl.value;
    let content = `${dosageForm} ${drug.name} (${drug.strength}${drug.unit}) ${frequency} ${duration} ${instruction}`;
    console.log(content);
  }
}
