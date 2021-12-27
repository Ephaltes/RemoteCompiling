import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';
import { FileCode } from '../file-module/file-code';
import * as globalVar from '../../../globals'
import { FileNode } from '../file-module/file-node';
export interface CompileCode {
  data: {
    compile?: { code: number, signal: any, stderr: "", stdout: "" },
    run: { code: number, signal: any, stderr: "", stdout: "" },
  };
}
export interface UserProject {
  data: {
    projects?: [{ projectName?: string, projectType?: number, files?: FileNode[] }]
  };
}
@Injectable()
export class CompileService {

  constructor(private http: HttpClient) { }
  public compile(version: string, code: CodeModel, runFiles: FileCode[]) {
    return this.http.post<CompileCode>(globalVar.apiURL + "/api/compile", { language: code.language, version: version, code: { args: [""], stdin: "", mainFile: code.uri, files: runFiles } });
  }
  public getProjects() {
    return this.http.get<UserProject>(globalVar.apiURL + "/api/user/getMySelf");
  }
}
