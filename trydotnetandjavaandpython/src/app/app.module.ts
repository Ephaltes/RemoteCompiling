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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ExercisePlatformExerciseOverviewTableComponent } from './exercise-platform-exercise-overview-table/exercise-platform-exercise-overview-table.component';
import { ExercisePlatformExerciseStudentTableComponent } from './exercise-platform-exercise-student-table/exercise-platform-exercise-student-table.component';
import { ExercisePlatformAddNewExerciseComponent } from './exercise-platform-add-new-exercise/exercise-platform-add-new-exercise.component';
import { LoginSiteComponent } from './login-site/login-site.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthInterceptor } from './interceptor/authentication-intercepter.service';
import { LoggedInUserComponent } from './logged-in-user/logged-in-user.component';
import { ExercisePlatformEditExerciseComponent } from './exercise-platform-edit-exercise/exercise-platform-edit-exercise.component';
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
    ExerciseCodeEditorComponent,
    ExercisePlatformExerciseOverviewTableComponent,
    ExercisePlatformExerciseStudentTableComponent,
    ExercisePlatformAddNewExerciseComponent,
    LoginSiteComponent,
    LoggedInUserComponent,
    ExercisePlatformEditExerciseComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule, MatGridListModule, MatTreeModule, MatIconModule, MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatCardModule, MatTableModule, MatPaginatorModule, MatCheckboxModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: LoginSiteComponent },
      { path: 'coding', component: CodingAppComponent },
      {
        path: 'platform', component: ExercisePlatformAppComponent, children: [
          { path: '', redirectTo: 'overview', pathMatch: 'full' },
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
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
