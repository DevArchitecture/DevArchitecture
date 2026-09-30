import { describe, it, expect, vi, beforeEach } from 'vitest'

const listMock = vi.fn()

vi.mock('@/services/api', () => ({
  crudApi: {
    list: (...args: unknown[]) => listMock(...args)
  }
}))

import { fetchAllRows } from '../pagination'

describe('fetchAllRows', () => {
  beforeEach(() => listMock.mockReset())

  it('unwraps paginated body and fetches every page', async () => {
    listMock
      .mockResolvedValueOnce({
        data: { success: true, data: [{ userId: 1 }], totalRecords: 3, pageNumber: 1 }
      })
      .mockResolvedValueOnce({
        data: { success: true, data: [{ userId: 2 }], totalRecords: 3, pageNumber: 2 }
      })
      .mockResolvedValueOnce({
        data: { success: true, data: [{ userId: 3 }], totalRecords: 3, pageNumber: 3 }
      })

    const rows = await fetchAllRows('/users')

    expect(rows).toEqual([{ userId: 1 }, { userId: 2 }, { userId: 3 }])
    expect(listMock).toHaveBeenCalledTimes(3)
    expect(listMock).toHaveBeenNthCalledWith(1, '/users', { pageNumber: 1, pageSize: 100 })
    expect(listMock).toHaveBeenNthCalledWith(2, '/users', { pageNumber: 2, pageSize: 100 })
  })

  it('returns bare array body as-is with a single request', async () => {
    listMock.mockResolvedValueOnce({ data: [{ id: 1 }, { id: 2 }] })

    const rows = await fetchAllRows('/groups')

    expect(rows).toEqual([{ id: 1 }, { id: 2 }])
    expect(listMock).toHaveBeenCalledTimes(1)
    expect(listMock).toHaveBeenCalledWith('/groups', { pageNumber: 1, pageSize: 100 })
  })

  it('stops after first page when totalRecords matches', async () => {
    listMock.mockResolvedValueOnce({
      data: { success: true, data: [{ userId: 1 }], totalRecords: 1, pageNumber: 1 }
    })

    const rows = await fetchAllRows('/users')

    expect(rows).toEqual([{ userId: 1 }])
    expect(listMock).toHaveBeenCalledTimes(1)
  })

  it('stops when a wrapped page comes back empty', async () => {
    listMock
      .mockResolvedValueOnce({
        data: { success: true, data: [{ userId: 1 }], totalRecords: 5, pageNumber: 1 }
      })
      .mockResolvedValueOnce({ data: { success: true, data: [], totalRecords: 5, pageNumber: 2 } })

    const rows = await fetchAllRows('/users')

    expect(rows).toEqual([{ userId: 1 }])
    expect(listMock).toHaveBeenCalledTimes(2)
  })
})
