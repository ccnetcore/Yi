import myaxios from '@/util/myaxios'
export default {
    getRole() {
        return myaxios({
            url: '/Role/getRole',
            method: 'get'
        })
    }
}