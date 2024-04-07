import React from 'react';
import { Select, Typography, Row, Col, Avatar, Card } from 'antd';
import moment from 'moment';

import { useGetCryptoNewsQuery } from '../services/cryptoNewsAPi';

const { Text, Title } = Typography;

const { Option } = Select;

const News = ({ simplified }) => {
  const { data: cryptoNews } = useGetCryptoNewsQuery({
    newsCategory: 'Cryptocurrency',
    count: simplified ? 6 : 12,
  });

  console.log(cryptoNews);

  const displayedNews = simplified ? cryptoNews?.data?.slice(0, 6) : cryptoNews?.data;

  return (
    <Row gutter={[24, 24]}>
      {displayedNews && displayedNews.length > 0 ? (
        displayedNews.map((news, i) => (
          <Col xs={24} sm={12} lg={8} key={i}>
            <Card hoverable className="news-card">
              <a href={news.url} target="_blank" rel="noreferrer">
                <div className="news-image-container">
                  <img
                    src={news.thumbnail}
                    alt={news.title}
                    style={{ width: '100%', height: '150px', objectFit: 'cover' }}
                  />
                  <Title className="news-title" level={4}>
                    {news.title}
                  </Title>
                </div>
                <p>{news.description.length > 200 ? `${news.description.substring(0, 200)}...` : news.description}</p>
                <div style={{ display: 'flex', alignItems: 'center', marginTop: '10px' }}>
                  <Avatar src={news.avatar} alt="Author" />
                  <Text style={{ marginLeft: '8px' }}>{moment(news.createdAt).fromNow()}</Text>
                </div>
              </a>
            </Card>
          </Col>
        ))
      ) : (
        <p>No news available</p>
      )}
    </Row>
  );
};

export default News;