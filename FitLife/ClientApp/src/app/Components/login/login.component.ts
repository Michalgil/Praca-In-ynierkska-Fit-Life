import { Constants } from './../../common/constans';
import { SignIn } from './../../Models/sign-in';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder} from '@angular/forms';
import { AccountService } from '../../services/Account/account.service';
import { SignInResult } from '../../Models/sign-in-result';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  formError: boolean;
  photo = '';
  constructor(
    private accountService: AccountService,
    private formBuider: FormBuilder,
    private router: Router) {
  }
  ngOnInit() {
    this.photo = '../../assets/Images/1cal.JPG'
    this.createValidationForm();

  }

  login() {
    this.accountService
      .login(this.getDataFromForm())
      .subscribe(signInResult => {
        localStorage.setItem(Constants.AUTH_TOKEN, signInResult.token),
        this.router.navigate(['/diet']);
      }, error => {
        this.formError = true;
      });
    
  }

  createValidationForm(){
    this.loginForm = this.formBuider.group({
      email: ['',[ Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  getDataFromForm(): SignIn{
    const modelFromForm = this.loginForm.value;
    return new SignIn(modelFromForm.email as string, modelFromForm.password as string);
  }


}
