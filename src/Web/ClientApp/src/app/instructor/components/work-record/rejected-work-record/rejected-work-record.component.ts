import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { InstructorClient, WorkRecordDto } from 'src/app/web-api-client';
import { WorkRecordDetailsComponent } from '../work-record-details/work-record-details.component';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-rejected-work-record',
  templateUrl: './rejected-work-record.component.html',
  styleUrls: ['./rejected-work-record.component.css']
})
export class RejectedWorkRecordComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['userName','startDate', 'endDate', 'totalHours', 'action'];
  dataSource: MatTableDataSource<WorkRecordDto> = new MatTableDataSource<WorkRecordDto>();
  userId: any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  WorkRecordList: any[] = [];


  constructor(private instructorClient: InstructorClient,private router: Router) {
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

  getWorkRecord(userId) {
    this.instructorClient.getRejectedWorkRecords23(userId).subscribe((responce) => {
      this.WorkRecordList = responce;
      this.dataSource = new MatTableDataSource(this.WorkRecordList);
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


  manageWorkRecord(data) {
    const navigationExtras: NavigationExtras = {
      state: {
        userId: this.userId,
        data: data
      }
    };
  
    this.router.navigate(['instructor/workrecord/details',data.id], navigationExtras);
  }



}

export interface SectionDtoData {
  id?: number;
  name?: string | undefined;
  courseRef?: number;
  startTime?: string;
  endTime?: string;
  courseId?: number;
  instructorId?: number;
  instructorName?: string | undefined;
  ta?: string | undefined;
  taId?: number | undefined;
}

