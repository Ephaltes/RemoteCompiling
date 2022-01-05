import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';
import { FileCode } from '../file-module/file-code';
import * as globalVar from '../../../globals'
export interface CompileCode {
  data: {
    compile?: { code: number, signal: any, stderr: "", stdout: "" },
    run: { code: number, signal: any, stderr: "", stdout: "" }
  };
}

@Injectable()
export class CompileService {

  constructor(private http: HttpClient) { }
  public compile(code: CodeModel, runFiles: FileCode[], args: string[], stdin: string) {
    return this.http.post<CompileCode>(globalVar.apiURL + "/api/compile", {language: code.language, code: { args: args, stdin: stdin, mainFile: code.uri, files: runFiles } });
  }
}
