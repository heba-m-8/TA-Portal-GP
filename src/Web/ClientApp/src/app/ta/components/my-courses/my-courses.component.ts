import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SectionDto, TAClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})
export class MyCoursesComponent implements OnInit,AfterViewInit  {
  displayedColumns: string[] = ['courseRef', 'name', 'startTime','instructorName'];
  dataSource: MatTableDataSource<SectionDto>;
  userId:any;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  sectionList:any[]=[];
  constructor(private TAClient: TAClient ) {
    this.userId = localStorage.getItem('UserId');
    this.getTASections(Number(this.userId));


  }

  ngOnInit(): void {
    this.getTASections(Number(this.userId));
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  


  getTASections(userId){
    this.TAClient.getTASections(userId).subscribe((responce) => {
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


}

