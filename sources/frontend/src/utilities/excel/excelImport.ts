export interface ImportColumn {
  field: string
  header?: string
  parser?: (value: any) => any
}

export interface GenerateTemplateOptions {
  columns: ImportColumn[]
  fileName?: string
  sheetName?: string
}

export interface ParseExcelOptions {
  file: File
  columns: ImportColumn[]
  sheetName?: string
}

function normalizeHeader(s: string) {
  return (s || '').toString().trim().toLowerCase()
}

export async function downloadImportTemplate(options: GenerateTemplateOptions) {
  const { columns, fileName = 'import_template.xlsx', sheetName = 'Sheet1' } = options
  let XLSX: any
  try {
    XLSX = await import('xlsx')
  } catch (err) {
    throw new Error("Không tìm thấy thư viện 'xlsx'")
  }
  const headers = columns.map(c => c.header || c.field)
  const ws = XLSX.utils.aoa_to_sheet([headers])
  const wb = XLSX.utils.book_new()
  XLSX.utils.book_append_sheet(wb, ws, sheetName)
  XLSX.writeFile(wb, fileName)
}

export async function parseExcelFile(options: ParseExcelOptions) {
  const { file, columns, sheetName } = options
  let XLSX: any
  try {
    XLSX = await import('xlsx')
  } catch (err) {
    throw new Error("Không tìm thấy thư viện 'xlsx'")
  }

  const buf = await file.arrayBuffer()
  const wb = XLSX.read(buf, { type: 'array' })
  const targetSheetName = sheetName && wb.Sheets[sheetName] ? sheetName : wb.SheetNames[0]
  const ws = wb.Sheets[targetSheetName]
  if (!ws) throw new Error('Không tìm thấy sheet hợp lệ trong file')

  // Read as objects using header row
  const rows: any[] = XLSX.utils.sheet_to_json(ws, { defval: '' })

  // Build header map from the first row keys (case-insensitive)
  const headerMap: Record<string, string> = {}
  if (rows.length > 0) {
    Object.keys(rows[0]).forEach(k => {
      headerMap[normalizeHeader(k)] = k
    })
  }

  const result = rows.map((r) => {
    const obj: Record<string, any> = {}
    for (const col of columns) {
      const header = col.header || col.field
      const key = headerMap[normalizeHeader(header)] || header
      const raw = r[key]
      obj[col.field] = col.parser ? col.parser(raw) : raw
    }
    return obj
  })

  return result
}
