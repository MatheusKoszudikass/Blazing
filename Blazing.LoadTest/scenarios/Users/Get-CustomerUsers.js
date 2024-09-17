import http from 'k6/http';
import { sleep } from 'k6';
import { Trend, Rate } from 'k6';
import { check, fail } from 'k6';

export default function () {
    let res = http.get('http://192.168.0.10:7239/api/User?page=1&pageSize=50');

    if (res === null || res === undefined) {
        fail('Request failed: response is null or undefined');
        return;
    }

    let durationMsg = `Max Duration ${1000 / 1000}s`;

    if (!check(res, {
        'max duration': (r) => r.timings.duration < 1000,
    })) {
        fail(durationMsg);
    }

    sleep(1);
}
