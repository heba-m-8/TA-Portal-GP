import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { PhdTAClient } from 'src/app/web-api-client';
import { ManageTasksComponent } from './manage-tasks/manage-tasks.component';

@Component({
  selector: 'app-phd-courses',
  templateUrl: './phd-courses.component.html',
  styleUrls: ['./phd-courses.component.css']
})
export class PhdCoursesComponent implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['courseRef', 'name', 'startTime','action'];
  dataSource: MatTableDataSource<SectionDtoData> = new MatTableDataSource<SectionDtoData>();
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  sectionList:any[]=[];
  constructor(private phdTAClient: PhdTAClient, private dialog: MatDialog ) {
    this.userId = localStorage.getItem('UserId');
    this.getSections(Number(this.userId));


  }

  ngOnInit(): void {
    this.getSections(Number(this.userId));
    
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  


  getSections(userId){
    this.phdTAClient.getPhdTASections(userId).subscribe((responce) => {
      this.sectionList = responce;
      this.dataSource = new MatTableDataSource(this.sectionList);
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


  manageTasks(section){

    const dialogRef = this.dialog.open(ManageTasksComponent, {
      maxHeight: '530px',
      width: '800px',
      data: { userId:this.userId ,section:section}
  })

  dialogRef.afterClosed().subscribe(result => {
  });


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
