import { TestBed, inject } from "@angular/core/testing";
import { LoginComponent } from "./login.component";
import { FormsModule } from "@angular/forms";
import { AuthService } from "./auth.service";
import { Router } from "@angular/router";
import { of } from "rxjs";


describe('LoginComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        LoginComponent
      ],
      imports: [
        FormsModule
      ],
      providers: [
        { provide: Router, useClass: RouterMock },
        { provide: AuthService, useClass: AuthMock }
      ]
    });
  });
  it('should create an instance', inject([Router, AuthService], (router: Router, authService: AuthService) => {
    const component = new LoginComponent(authService, router);
    expect(component).toBeTruthy();
  }));
  describe('when the login page loads', () => {
    it('then the login name should be defaulted', inject([Router, AuthService], (router: Router, authService: AuthService) => {
      const component = new LoginComponent(authService, router);
      expect(component.form.value.username).toEqual('');
    }));
    it('then the password should be defaulted', inject([Router, AuthService], (router: Router, authService: AuthService) => {
        const component = new LoginComponent(authService, router);
        expect(component.form.value.password).toEqual('');
      }));
  });
  describe('when a valid username and password are entered', () => {
    it('then the home route should be displayed', inject([Router, AuthService], (router: Router, authService: AuthService) => {
      spyOn(authService, 'login').and.returnValue(of(true));
      spyOn(router, 'navigateByUrl').and.returnValue(new Promise<boolean>(() => {return true;}));
      const component = new LoginComponent(authService, router);
      component.form.controls['username'].setValue('test@test.com');
      component.form.controls['password'].setValue('1234567');
      component.submit();
      expect(router.navigateByUrl).toHaveBeenCalled();
    }));
  });
  describe('when an invalid username and password are entered', () => {
    it('then Login Failed should be displayed', inject([Router, AuthService], (router: Router, authService: AuthService) => {
      spyOn(authService, 'login').and.returnValue(of(false));
      spyOn(router, 'navigateByUrl').and.returnValue(new Promise<boolean>(() => {return false;}));
      const component = new LoginComponent(authService, router);
      component.submit();
      expect(router.navigateByUrl).not.toHaveBeenCalled();
    }));
  });
});

class AuthMock {
    login(username: string, password: string) { return true; }
}

class RouterMock {
  navigateByUrl(url: string) { return url; }
}