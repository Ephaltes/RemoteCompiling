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
import { ShContextMenuModule } from 'ng2-right-click-menu';
import { ErrorDialogComponent } from './error-dialog/error-dialog.component'
import { ToasterModule } from 'angular2-toaster';
import { FolderOptionDialogComponent } from './folder-option-dialog/folder-option-dialog.component';
import { AddNewFolderComponent } from './add-new-folder/add-new-folder.component';
import { CodingAppComponent } from './coding-app/coding-app.component';
import { RouterModule } from '@angular/router';
import { ExercisePlatformAppComponent } from './exercise-platform-app/exercise-platform-app.component';
import { ExercisePlatformNavigationComponent } from './exercise-platform-navigation/exercise-platform-navigation.component';
import { ExercisePlatformCreateComponent } from './exercise-platform-create/exercise-platform-create.component';
import { ExercisePlatformCorrectComponent } from './exercise-platform-correct/exercise-platform-correct.component';
import { ExercisePlatformOverviewComponent } from './exercise-platform-overview/exercise-platform-overview.component';
import { ExerciseCodeEditorComponent } from './exercise-code-editor/exercise-code-editor.component';


@NgModule({
  declarations: [
    AppComponent,
    AddNewFileComponent,
    ErrorDialogComponent,
    FolderOptionDialogComponent,
    AddNewFolderComponent,
    CodingAppComponent,
    ExercisePlatformAppComponent,
    ExercisePlatformNavigationComponent,
    ExercisePlatformCreateComponent,
    ExercisePlatformCorrectComponent,
    ExercisePlatformOverviewComponent,
    ExerciseCodeEditorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule, MatGridListModule, MatTreeModule, MatIconModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatCardModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'coding' },
      { path: 'coding', component: CodingAppComponent },
      {
        path: 'platform', component: ExercisePlatformAppComponent, children: [
          {path: '', redirectTo: 'overview', pathMatch: 'full'},
          { path: 'overview', component: ExercisePlatformOverviewComponent },
          { path: 'create', component: ExercisePlatformCreateComponent },
          { path: 'correct', component: ExercisePlatformCorrectComponent },
        ]
      }
    ]),
    MatSelectModule, FileUploadModule, ShContextMenuModule, ToasterModule.forRoot(),
    CodeEditorModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
