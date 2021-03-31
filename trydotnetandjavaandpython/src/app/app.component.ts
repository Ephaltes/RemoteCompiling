import { Component } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'trydotnetandjavaandpython';
  theme = 'vs-dark';

  codeModel: CodeModel = {
    language: 'csharp',
    uri: 'Program.cs',
    value: '',
  };

  options = {
    contextmenu: true,
    minimap: {
      enabled: true,
    },
  };

  onCodeChanged(value: any) {
    console.log('CODE', value);
  }
}
