import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { FinanceClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-confirm-reject-work-record',
  templateUrl: './confirm-reject-work-record.component.html',
  styleUrls: ['./confirm-reject-work-record.component.css']
})
export class ConfirmRejectWorkRecordComponent implements OnInit{
  userId:any;
  note:any;

  
  tasksList:any[]=[];
  totalHours:any;
  constructor( @Inject(MAT_DIALOG_DATA) private data: any,private financeClient: FinanceClient,private router: Router) { }

  ngOnInit(): void {
    this.userId = Number(this.data.userId);
  }



  reject(){
    this.financeClient.rejectWorkRecord22(this.data.workRecordId,this.note).subscribe((responce) => {
      this.router.navigate(['/finance']);
    });
  }
}
