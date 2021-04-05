import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';
import { FileNode } from '../file-node';
export interface CompileCode {
  data: {
    compile?: { code: number, signal: any, stderr: "", stdout: "" },
    run: { code: number, signal: any, stderr: "", stdout: "" }
  };
}

@Injectable()
export class CompileService {

  constructor(private http: HttpClient) { }
  public compile(version: string, code: CodeModel) {
    //return this.http.get(`/Api/Help/runtimes`);
    return this.http.post<CompileCode>(`/Api/Compile`, { language: code.language, version: version, args: [""], stdin: "", mainFile: code.uri, files: [{ name: code.uri, content: code.value }] });
  }
}
