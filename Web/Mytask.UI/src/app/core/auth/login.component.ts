import { Component } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "./auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    form: FormGroup = new FormGroup({
        username: new FormControl(''),
        password: new FormControl(''),
      });

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  submit() {
    if (this.form.valid) {
      const val = this.form.value;

      if (val.username && val.password) {
        this.authService.login(val.username, val.password).subscribe(() => {
          this.router.navigateByUrl('/');
        });
      }
    }
  }
}