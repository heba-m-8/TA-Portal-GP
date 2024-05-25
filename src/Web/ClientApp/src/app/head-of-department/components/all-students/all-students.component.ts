import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HODClient, TADto } from 'src/app/web-api-client';

@Component({
  selector: 'app-all-students',
  templateUrl: './all-students.component.html',
  styleUrls: ['./all-students.component.css']
})
export class AllStudentsComponent implements OnInit,AfterViewInit {
  displayedColumns: string[] = ['universityId','userName','userEmail','gpa','scholarship'];
  dataSource: MatTableDataSource<TADto>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  taList:any[]=[];
  constructor(private hodClint: HODClient  ) {
  }

  ngOnInit(): void {
    let userId = localStorage.getItem('UserId');
    this.GetTAs(Number(userId));
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  GetTAs(userId){
    this.hodClint.getTAsDetails(userId).subscribe((responce) => {
      this.taList = responce;
      this.dataSource = new MatTableDataSource(this.taList);
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
