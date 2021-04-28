import {
    HTTP
} from '../utils/http.js'
class Pro extends HTTP {
    url = 'Pro.ashx'
    // 传入参数
    // success: (e) => {}
    getNavbarData(success) {
        this.request({
            url: this.url,
            data: {
                Do: 'GetNavbarData'
            },
            success: success
        })
    }

    // 传入参数
    // params: { 
    //     data: { uid },
    //     success: (e) => {}
    // }
    getCartCount(params) {
        this.request({
            url: this.url,
            data: {
                Do: 'GetCartCount',
                uid: params.data.uid
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { pid, uid, price },
    //     success: (e) => {}
    // }
    plus(params) {        
        //console.log(params)
        this.request({
            url: this.url,
            data: {
                do: 'Plus',
                uid: params.data.uid,
                pid: params.data.pid,
                price: params.data.price
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { pid, uid },
    //     success: (e) => {}
    // }
    minus(params) {
        //console.log(params)
        this.request({
            url: this.url,
            data: {
                do: 'Minus',
                uid: params.data.uid,
                pid: params.data.pid
            },
            success: params.success
        })
    }

    // 传入参数
    // success: (e) => {}
    getSliderData(success) {
        this.request({
            url: this.url,
            data: {
                Do: 'GetCommonData',
                type: 'slider'
            },
            success: success
        })
    }

    // 传入参数
    // success: (e) => {}
    getEntryData(success) {
        this.request({
            url: this.url,
            data: {
                Do: 'GetCommonData',
                type: 'entry'
            },
            success: success
        })
    }

    // 传入参数
    // params: { 
    //     data: { 自定义 },
    //     success: (e) => {}
    // }
    getProData(params) {
        params.url = this.url
        if (!params.data) {
            params.data = {}
        }
        params.data.do = 'GetProData'
        //console.log(params)
        this.request(params)
    }

    // 传入参数
    // success: (e) => {}
    getSortData(success) {
        this.request({
            url: this.url,
            data: {
                Do: 'GetSortData'
            },
            success: success
        })
    }

    getCartData (params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetCartData',
                uid: params.data.uid
            },
            success: params.success
        })
    }
}
export{
    Pro
}