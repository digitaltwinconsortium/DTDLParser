
import { DtdlObjectModel } from "./generated/DtdlOm";
export * from "./generated/DtdlOm";
export * from "./DtdlErr"

declare module "dtdl-parser-interop" {
    export function parseAsync(jsonTexts: string | string[]): Promise<DtdlObjectModel>
}