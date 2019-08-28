import { TrainingService } from './../../services/Training/training.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { TrainingData } from '../../Models/training.data';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css']
})
export class TrainingComponent implements OnInit {
  trainingForm: FormGroup;
  formError: boolean;
  crateTrainingForm: boolean;
  trainingPlan: any[] = [];
  numberOfTraining: number;


  constructor(
     private formBuilder: FormBuilder,
     private trainingService: TrainingService
  ) { }

  ngOnInit() {
    this.crateTrainingForm = true;
    this.createTrainingForm();


  }

  createTrainingForm(){
    this.trainingForm = this.formBuilder.group({
      priorityPart: ['', Validators.required],
      experience: ['', Validators.required],
    });
  }

  createTraning(){
    this.trainingService.createTraining(this.getTrainingtDataFromForm())
      .subscribe(result => {
        this.crateTrainingForm = false;
        this.trainingPlan = result;
      }, error => {
        this.formError = true;
      });
  }

  getTrainingtDataFromForm(): TrainingData{
    const trainingModel = this.trainingForm.value;

    return new TrainingData(trainingModel.experience as number, trainingModel.priorityPart as string);
  }

  setTraining(numberOfTraining: number){
    this.numberOfTraining = numberOfTraining;
    console.log(this.trainingPlan[this.numberOfTraining]);
  }


  

}
