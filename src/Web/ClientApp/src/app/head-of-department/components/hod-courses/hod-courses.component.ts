import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HODClient, SectionDto } from 'src/app/web-api-client';
import { ManageTAComponent } from '../manage-ta/manage-ta.component';

@Component({
  selector: 'app-hod-courses',
  templateUrl: './hod-courses.component.html',
  styleUrls: ['./hod-courses.component.css']
})
export class HODCoursesComponent implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['courseRef', 'name', 'startTime','instructorName','ta','action'];
  dataSource: MatTableDataSource<SectionDto>;
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  sectionList:any[]=[];
  constructor(private hodClint: HODClient, private dialog: MatDialog ) {
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
    this.hodClint.getSections(userId).subscribe((responce) => {
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

  manageTA(sectionId,taId){
    const dialogRef = this.dialog.open(ManageTAComponent, {
      height: '250px',
      width: '500px',
      data: { userId:this.userId ,sectionId:sectionId, taId:taId}
  })

  dialogRef.afterClosed().subscribe(result => {
    this.getSections(Number(this.userId));
    });

    dialogRef.componentInstance.sectionUpdated.subscribe(() => {
      this.getSections(Number(this.userId));
    });
  }

}
