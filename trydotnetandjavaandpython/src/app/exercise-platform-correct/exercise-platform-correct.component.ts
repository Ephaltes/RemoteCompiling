import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CodeEditorComponent, CodeModel } from '@ngstack/code-editor';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
  action: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H', action: 'Delete' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He', action: 'Delete' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li', action: 'Delete' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be', action: 'Delete' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B', action: 'Delete' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C', action: 'Delete' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N', action: 'Delete' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O', action: 'Delete' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F', action: 'Delete' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne', action: 'Delete' },
];
@Component({
  selector: 'app-exercise-platform-correct',
  templateUrl: './exercise-platform-correct.component.html',
  styleUrls: ['./exercise-platform-correct.component.scss']
})

export class ExercisePlatformCorrectComponent implements OnInit {
  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol', 'action'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  exerciseSelected = false;
  selectedItem: CodeModel = {language:"", value:"", uri:""};
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor() { }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  openCodingApp(row:any){
    console.log(row);
    this.exerciseSelected=true;
    this.selectedItem.value=row.name;
  }

}
