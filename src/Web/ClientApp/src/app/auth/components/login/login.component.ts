import { Component, OnInit } from '@angular/core';
import { AuthClient, LoginDto } from 'src/app/web-api-client';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  universityId = new FormControl('', [Validators.required]);
  userPassword = new FormControl('', [Validators.required, Validators.minLength(4),]);

  constructor(private router: Router, private authClient: AuthClient, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  submit() {
    var loginDto = new LoginDto();
    loginDto.universityId = this.universityId.value.toString();
    loginDto.userPassword = this.userPassword.value.toString();

    var responce1: any;

    this.authClient.login(loginDto).subscribe(
      (res) => {
        if (res != null) {
          responce1 = res;
          const responce = {
            token: responce1.toString(),
          };
          localStorage.setItem('token', responce.token);
          let data: any = jwtDecode(responce.token);
          localStorage.setItem('user', JSON.stringify({ ...data }));
          localStorage.setItem('UserId', data.ID);
          if (data.role == 'TA') this.router.navigate(['/ta']);
          else if (data.role == 'Instructor') this.router.navigate(['/instructor']);
          else if (data.role == 'HOD') this.router.navigate(['/hod']);
          else if (data.role == 'Dean') this.router.navigate(['/dean']);
          else if (data.role == 'DeanOfGraduateStudies') this.router.navigate(['/deanofgraduatestudies']);
          else if (data.role == 'Finance') this.router.navigate(['/finance']);
          else if (data.role == 'TAPHD') this.router.navigate(['/taphd']);
        } else {
          this.toastr.warning('To access your account, please register first.');
        }
      },
      (error) => {
        // notify('Wrong Email or password');
      }
    );
  }

  goToReg() {
    this.router.navigate(['register']);
  }

  togglePasswordVisibility() {
    var passwordInput = document.getElementById('password-input');
    var passwordToggleIcon = document.getElementById('password-toggle-icon');

    if (passwordInput instanceof HTMLInputElement) {
      if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordToggleIcon.classList.remove('fa-eye');
        passwordToggleIcon.classList.add('fa-eye-slash');
      } else {
        passwordInput.type = 'password';
        passwordToggleIcon.classList.remove('fa-eye-slash');
        passwordToggleIcon.classList.add('fa-eye');
      }
    }
  }
}
