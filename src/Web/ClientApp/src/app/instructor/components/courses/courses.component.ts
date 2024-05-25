import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { InstructorClient, SectionDto } from 'src/app/web-api-client';
import { ManageTasksComponent } from '../manage-tasks/manage-tasks.component';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent  implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['courseRef', 'name', 'startTime','ta','action'];
  dataSource: MatTableDataSource<SectionDtoData> = new MatTableDataSource<SectionDtoData>();
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  sectionList:any[]=[];
  constructor(private instructorClient: InstructorClient, private dialog: MatDialog ) {
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
    this.instructorClient.getInstructorSections(userId).subscribe((responce) => {
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


  manageTasks(sectionId,taId){
    const dialogRef = this.dialog.open(ManageTasksComponent, {
      maxHeight: '530px',
      width: '800px',
      data: { userId:this.userId ,sectionId:sectionId, taId:taId}
  })

  dialogRef.afterClosed().subscribe(result => {
    this.getSections(Number(this.userId));
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