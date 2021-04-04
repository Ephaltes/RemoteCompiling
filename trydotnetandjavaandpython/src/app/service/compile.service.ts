import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';
import { FileNode } from '../file-node';
export interface CompileCode {
  args?: string[];
  stdin?: string;
  mainFile: string;
  files: CodeModel[];
}

@Injectable()
export class CompileService {

  constructor(private http: HttpClient) { }
  public compile(version: string, code: CodeModel) {
    //return this.http.get(`/Api/Help/runtimes`);
    return this.http.post<CompileCode>(`/Api/Compile/${code.language}/${version}`, { args: [], stdin: "",   mainFile: "Program.cs", files: [{ name: code.uri, content: code.value }] });
  }
}
