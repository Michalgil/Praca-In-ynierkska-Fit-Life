import { DietService } from './../../services/Diet/diet.service';
import { Observable } from 'rxjs';
import { DietData } from './../../Models/diet.data.';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Diet } from '../../Models/diet';

@Component({
  selector: 'app-diet',
  templateUrl: './diet.component.html',
  styleUrls: ['./diet.component.css']
})
export class DietComponent implements OnInit {
  dietForm: FormGroup;
  formError: boolean;
  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private dietService: DietService
  ) { }

  ngOnInit() {
    this.createDietForm()
  }

  createDietForm(){
    this.dietForm = this.formBuilder.group({
      weight: ['', Validators.required],
      height: ['', Validators.required],
      age: ['', Validators.required],
      dailyActivity: ['', Validators.required],
      dietTarget: ['', Validators.required]
    });
  }

  createDiet(){
    this.dietService
      .createDiet(this.getDataFromForm())
      .subscribe(diet => {
        //diet data
      }, error => {
        this.formError = true;
      });
  }

  getDataFromForm() : DietData{
    const dietModel = this.dietForm.value;
    return new DietData(dietModel.weight as number,dietModel.height as number, dietModel.age as number, dietModel.dailyActivity as number, dietModel.dietTarget as number);
  }

}
