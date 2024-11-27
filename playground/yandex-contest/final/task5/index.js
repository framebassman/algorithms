function isValidYandexLink1(url) {
  url = url.toLowerCase();
  let array = [];
  array = url.split('://')

  if (array.length > 1) {
    url = array[1]
  } else {
    url = array[0];
  }

  url = url.split('/')[0];

  const portCandidates = url.split(':');
  if (portCandidates.length > 1) {
    url = portCandidates[0];
  }

  let names = /^([a-zA-Z0-9]([-a-zA-Z0-9]{0,61}[a-zA-Z0-9])?\.)?([a-zA-Z0-9]([-a-zA-Z0-9]{0,252}[a-zA-Z0-9])?)\.([a-zA-Z]{2,63})$/;  
  if (!names.test(url)) {
    return false;
  }

  const res = url.split('.');
  if (res[res.length - 2] === 'ya' || res[res.length - 2] === 'yandex') {
    return true;
  }

  return false;
}

function isValidYandexLink(url) {
  try {
      // Попробуем создать объект URL, если не получится — URL невалиден
      const parsedUrl = new URL(url);

      // Список разрешенных доменов Яндекса
      const yandexDomains = [
          'ya.ru',
          'yandex.ru',
          'yandex.com',
          'yandex.kz',
          'yandex.by',
          'yandex.az',
          'yandex.ua',
          'yandex.net',
          'yandex.tj',
          'yandex.uz',
      ];

      // Проверка, что домен или поддомен относится к одному из сервисов Яндекса
      return yandexDomains.some(domain => parsedUrl.hostname.endsWith(domain));
  } catch (e) {
      // Если ошибка, значит URL некорректен
      return false;
  }
}

console.log(isValidYandexLink("ya.ru:80"));

const validDomainNames = [
  "https://ya.ru",
  "https://education.yandex.ru",
  "http://yandex.ru/cup",
  "https://dataschool.yandex.com",
  "https://education.yandex.ru/uchebnik",
  "ya.ru",
  "ya.ru:80",
]

const invalidDomainNames = [
  "https://google.com",
  "http://example.com",
  "hts://y*ndex.ru/somepath",
  "hts://y*ndex.ru/somepath",
  "1",
  "team.yandex",
  "yandex",
  "mailto:johndoe@yandex.com",
  "t.yandex",
  "---t.yandex.ru",
  "htp:/wrong.url"
]

import { expect } from 'chai';

it("Test Domain Valid Names RegExp", () => {
  validDomainNames.forEach((val) => {
      it(`Text: ${val}`, () => {
          expect(isValidYandexLink(val)).to.be.true;
      });
  });
});

it("Test Domain Invalid Names RegExp", () => {
  invalidDomainNames.forEach((val) => {
      it(`Text: ${val}`, () => {
          expect(isValidYandexLink(val)).to.be.false;
      });
  });
});
