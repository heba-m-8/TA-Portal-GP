import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AuthClient, RegisterDto } from 'src/app/web-api-client';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  id: any;
  universityId = new FormControl('', [Validators.required, Validators.minLength(4)]);
  userPassword = new FormControl('', [Validators.required, Validators.minLength(4),]);

  constructor( public router: Router, private authClient: AuthClient, private title: Title,private toastr: ToastrService ) {
  }

  ngOnInit(): void {}



  async submit() {
    await new Promise((f) => setTimeout(f, 1000));

    var registerDto = new RegisterDto();
    registerDto.universityId = this.universityId.value;
    registerDto.userPassword = this.userPassword.value;

    this.authClient.register(registerDto).subscribe((responce) => {
      this.id = responce;
      if (this.id > 0) {
        this.toastr.success('User registration successful'); 
        this.router.navigate(['']);
      }
      else if(this.id == -1)
      this.toastr.warning("This university ID is already registered.");
      else {
        this.toastr.warning("The university ID or password is incorrect.");

      }
    });
  }


  goToLogin() {
    this.router.navigate(['']);
  }
}
