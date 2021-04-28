class HTTP {
    request(params) {
        if (!params.url) {
            params.url = 'Pro.ashx'
        }
        if (!params.method) {
            params.method = "GET"
        }
        if (!params.header) {
            params.header = {
                'content-Type': 'application/json'
            }
        }
        if (!params.dataType) {
            params.dataType = "json"
        }
        wx.request({
            url: 'http://localhost:58843/' + params.url,
            method: params.method,
            data: params.data,
            header: params.header,
            dataType: params.dataType,
            success: (e) => {
                params.success(e)
            }
        })
    }
}
export {
    HTTP
}