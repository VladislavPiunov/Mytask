import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "./auth.service";

@Component({
    selector: 'app-login',
    template: `
  <form [formGroup]="form">
      <fieldset>
          <legend>Login</legend>
          <div class="form-field">
              <span>Username:</span>
              <input name="username" formControlName="username">
          </div>
          <div class="form-field">
              <span>Password:</span>
              <input name="password" formControlName="password" 
                     type="password">
          </div>
      </fieldset>
      <div class="form-buttons">
          <button class="button button-primary" 
                  (click)="login()">Login</button>
      </div>
  </form>`})
  export class LoginComponent {
      form:FormGroup;
  
      constructor(private fb:FormBuilder, 
                   private authService: AuthService, 
                   private router: Router) {
  
          this.form = this.fb.group({
              username: ['',Validators.required],
              password: ['',Validators.required]
          });
      }
  
      login() {
          const val = this.form.value;
  
          if (val.username && val.password) {
              this.authService.login(val.username, val.password)
                  .subscribe(
                      () => {
                          this.router.navigateByUrl('/');
                      }
                  );
          }
      }
  }