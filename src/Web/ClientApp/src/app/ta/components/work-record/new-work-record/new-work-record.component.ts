import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { WorkRecordDto,TAClient } from 'src/app/web-api-client';
import { WorkRecordDetailsComponent } from '../work-record-details/work-record-details.component';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-new-work-record',
  templateUrl: './new-work-record.component.html',
  styleUrls: ['./new-work-record.component.css']
})
export class NewWorkRecordComponent  implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['startDate', 'endDate', 'totalHours','action'];
  dataSource: MatTableDataSource<WorkRecordDto>;
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  workRecordList:any[]=[];
  constructor(private TAClient: TAClient,private router: Router) {
    this.userId = localStorage.getItem('UserId');
    this.getWorkRecord(Number(this.userId));


  }

  ngOnInit(): void {
    this.getWorkRecord(Number(this.userId));
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  


  getWorkRecord(userId){
    this.TAClient.getWorkRecord22(userId,false).subscribe((responce) => {
      this.workRecordList = responce;
      this.dataSource = new MatTableDataSource(this.workRecordList);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }






  manageWorkRecord(data){

    const navigationExtras: NavigationExtras = {
      state: {
        userId: this.userId,
        data: data
      }
    };
  
    this.router.navigate(['ta/workrecord/details',data.id], navigationExtras);
  }

}
