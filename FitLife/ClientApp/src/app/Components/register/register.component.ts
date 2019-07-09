import { Router } from '@angular/router';
import { RegisterModel } from './../../Models/register-model';
import { AccountService } from './../../services/Account/account.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as $ from 'jquery';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  formError: boolean;
  gender = [
    {id: 1, isMale: true, name: "Mężczyzna"},
    {id: 2, isMale: false, name: "Kobieta"}
  ];
  selectedGenderValue = null;
  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router) {
     }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.formBuilder.group({
      email: ['',[ Validators.required, Validators.email]],
      password: ['', Validators.required],
      repeatPassword: ['', Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      selectedGender: ['', Validators.required]
    });
  }

  register(){
    this.accountService
    .register(this.getDataFromForm())
    .subscribe(result =>{
      this.router.navigate(['/login'])
    }, error => {
      this.formError = true;
    })
  }

  getDataFromForm(): RegisterModel{
    const registerFormModel = this.registerForm.value;
    return new RegisterModel(registerFormModel.name as string, registerFormModel.surname as string, registerFormModel.password as string, registerFormModel.email as string, this.selectedGenderValue.isMale as boolean);
  }

  onChangeGender(genderValue){
    this.selectedGenderValue = genderValue;
  }

}