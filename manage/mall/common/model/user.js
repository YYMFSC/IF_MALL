import {
    HTTP
} from '../utils/http.js'
class User extends HTTP {
    url = 'User.ashx'

    login(params) {
        this.request({
            url: this.url,
            data: {
                do: 'Login',
                code: params.data.code
            },
            success: params.success
        })
    }
    // 传入参数
    // params: { 
    //     data: { uid },
    //     success: (e) => {}
    // }
    getAddress(params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetAddress',
                uid: params.data.uid
            },
            success: params.success
        })
    }
    getAddressOne(params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetAddressOne',
                id: params.data.id
            },
            success: params.success
        })
    }
}
export {
    User
}