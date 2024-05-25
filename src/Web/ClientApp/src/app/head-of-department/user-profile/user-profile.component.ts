import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthClient, ChangePasswordDto, UserDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})


export class UserProfileComponent implements OnInit {
  userId:any;
  user:UserDto = new UserDto();
  currentp:any='';
  newp:any='';
  exisit:boolean;
  constructor(private authClient: AuthClient,private toastr: ToastrService ) {
    this.userId = localStorage.getItem('UserId');
    this.getTASections();


  }

  ngOnInit(): void {
  }


  getTASections(){
    this.authClient.getUserDetails(this.userId).subscribe((responce) => {
      this.user = responce;
    });

  }

  changePassord(){
    var changePasswordDto = new ChangePasswordDto();
    changePasswordDto.newPassword = this.newp;
    changePasswordDto.userId = this.userId;
    this.authClient.changePassword(changePasswordDto).subscribe((responce) => {
      this.toastr.success('Password changed successfully!'); 
      this.currentp = '';
      this.newp='';
    });
  }


  checkUser(e){

    this.authClient.checkPassowrd(this.userId,e.target.value).subscribe((responce) => {
      this.exisit= responce



    });
  }
}