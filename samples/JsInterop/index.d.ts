
import { DtdlObjectModel } from "./generated/DtdlOm";
export * from "../../javascript/generated/DtdlOm";
export * from "../../javascript/DtdlErr"

declare module "dtdl-parser-interop" {
    export function parseAsync(jsonTexts: string | string[]): Promise<DtdlObjectModel>
}