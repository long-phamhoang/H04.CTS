export interface ExportColumn {
  field: string
  header?: string
  formatter?: (value: any, row: any) => any
}

export interface ExportToExcelOptions {
  data: any[]
  columns: ExportColumn[]
  fileName?: string
  sheetName?: string
}

export async function exportToExcel(options: ExportToExcelOptions) {
  const { data, columns, fileName = `export_${new Date().toISOString().slice(0, 10)}.xlsx`, sheetName = 'Sheet1' } = options

  if (!Array.isArray(data) || data.length === 0) {
    throw new Error('No data to export')
  }
  if (!Array.isArray(columns) || columns.length === 0) {
    throw new Error('No columns selected')
  }

  let XLSX: any
  try {
    XLSX = await import('xlsx')
  } catch (err) {
    throw new Error("Không tìm thấy thư viện 'xlsx'")
  }

  // Chuẩn hóa dữ liệu theo cột đã chọn
  const rows = data.map((row) => {
    const output: Record<string, any> = {}
    for (const col of columns) {
      const header = col.header ?? col.field
      const raw = row[col.field]
      output[header] = col.formatter ? col.formatter(raw, row) : raw ?? ''
    }
    return output
  })

  const ws = XLSX.utils.json_to_sheet(rows)
  const wb = XLSX.utils.book_new()
  XLSX.utils.book_append_sheet(wb, ws, sheetName)
  XLSX.writeFile(wb, fileName)
}
