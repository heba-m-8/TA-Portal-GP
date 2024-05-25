import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HODClient, ManageTADto } from 'src/app/web-api-client';

@Component({
  selector: 'app-manage-ta',
  templateUrl: './manage-ta.component.html',
  styleUrls: ['./manage-ta.component.css']
})
export class ManageTAComponent implements OnInit{
  taList:any[]=[];
  selectedTA:any;
  @Output() sectionUpdated: EventEmitter<void> = new EventEmitter<void>();

  constructor( @Inject(MAT_DIALOG_DATA) private data: any,private hodClint: HODClient) { }

  ngOnInit(): void {

    this.getTAs(this.data.userId);
  }



  getTAs(userId){
    this.hodClint.getTAsNamesIds(userId,this.data.sectionId).subscribe((responce) => {
      this.taList = responce;
      this.selectedTA=this.data.taId;
    });
  }

  manageTA(){
    let manageTADto = new ManageTADto();
      manageTADto.sectionId = this.data.sectionId;
      manageTADto.taId = this.selectedTA;
    this.hodClint.manageTAs(manageTADto).subscribe((responce) => {
      this.sectionUpdated.emit();

    });
  }
}
