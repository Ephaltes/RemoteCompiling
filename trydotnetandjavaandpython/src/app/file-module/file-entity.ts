import { CheckPoint } from "./checkpoint";

export class FileEntity {
    id: number;
    lastModified: Date;
    fileName: string;
    checkpoints: CheckPoint[]
}