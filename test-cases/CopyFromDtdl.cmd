rd /s /q specification
rd /s /q generated
rd /s /q doc-examples

xcopy /s /y ..\..\DTDL\test-cases\specification specification\
xcopy /s /y ..\..\DTDL\test-cases\generated generated\
xcopy /s /y ..\..\DTDL\test-cases\doc-examples doc-examples\
