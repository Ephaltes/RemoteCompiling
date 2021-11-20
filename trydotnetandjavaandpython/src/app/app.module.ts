import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatGridListModule } from '@angular/material/grid-list';
import { CodeEditorModule } from '@ngstack/code-editor';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { FileUploadModule } from '@iplab/ngx-file-upload';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { AddNewFileComponent } from './add-new-file/add-new-file.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {ShContextMenuModule} from 'ng2-right-click-menu';
import { ErrorDialogComponent } from './error-dialog/error-dialog.component'
import { ToasterModule } from 'angular2-toaster';
import { FolderOptionDialogComponent } from './folder-option-dialog/folder-option-dialog.component';
import { AddNewFolderComponent } from './add-new-folder/add-new-folder.component';


@NgModule({
  declarations: [
    AppComponent,
    AddNewFileComponent,
    ErrorDialogComponent,
    FolderOptionDialogComponent,
    AddNewFolderComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule, MatGridListModule, MatTreeModule, MatIconModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatCardModule,
    MatSelectModule,FileUploadModule, ShContextMenuModule,ToasterModule.forRoot(),
    CodeEditorModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
